
import Dict exposing (Dict)
import Html exposing (Html, Attribute, a, div, hr, input, span, text)
import Html.Attributes exposing (..)
import Html.Events exposing (..)
import Http
import Json.Decode as Json exposing ((:=))
import Navigation
import String
import Task
import UrlParser exposing (Parser, (</>), format, int, oneOf, s, string)



main =
  Navigation.program (Navigation.makeParser hashParser)
    { init = init
    , view = view
    , update = update
    , urlUpdate = urlUpdate
    , subscriptions = subscriptions
    }



-- URL PARSERS - check out evancz/url-parser for fancier URL parsing


toHash : Page -> String
toHash page =
  case page of
    Home ->
      "#home"

    Blog id ->
      "#blog/" ++ toString id

    Search query ->
      "#search/" ++ query


hashParser : Navigation.Location -> Result String Page
hashParser location =
  UrlParser.parse identity pageParser (String.dropLeft 1 location.hash)


type Page = Home | Blog Int | Search String


pageParser : Parser (Page -> a) a
pageParser =
  oneOf
    [ format Home (s "home")
    , format Blog (s "blog" </> int)
    , format Search (s "search" </> string)
    ]



-- MODEL


type alias Model =
  { page : Page
  , query : String
  , cache : Dict String (List String)
  }


init : Result String Page -> (Model, Cmd Msg)
init result =
  urlUpdate result (Model Home "" Dict.empty)



-- UPDATE


type Msg
  = Query String
  | Enter
  | FetchFailure String Http.Error
  | FetchSuccess String (List String)


{-| A relatively normal update function. The only notable thing here is that we
are commanding a new URL to be added to the browser history. This changes the
address bar and lets us use the browser&rsquo;s back button to go back to
previous pages.
-}
update : Msg -> Model -> (Model, Cmd Msg)
update msg model =
  case msg of
    Query query ->
      { model | query = query }
        ! []

    Enter ->
      let
        newPage =
          Search model.query
      in
        { model | page = newPage }
          ! [ Navigation.newUrl (toHash newPage) ]

    FetchFailure query _ ->
      { model | cache = Dict.insert query [] model.cache }
        ! []

    FetchSuccess query locations ->
      { model | cache = Dict.insert query locations model.cache }
        ! []


{-| The URL is turned into a result. If the URL is valid, we just update our
model to the new count. If it is not a valid URL, we modify the URL to make
sense.
-}
urlUpdate : Result String Page -> Model -> (Model, Cmd Msg)
urlUpdate result model =
  case Debug.log "result" result of
    Err _ ->
      ( model, Navigation.modifyUrl (toHash model.page) )

    Ok (Search query as page) ->
      { model
        | page = page
        , query = query
      }
        ! if Dict.member query model.cache then [] else [ get query ]

    Ok page ->
      { model
        | page = page
        , query = ""
      }
        ! []


get : String -> Cmd Msg
get query =
  Task.perform (FetchFailure query) (FetchSuccess query) <|
    Http.get places ("http://api.zippopotam.us/us/" ++ query)


places : Json.Decoder (List String)
places =
  let
    place =
      Json.object2 (\city state -> city ++ ", " ++ state)
        ("place name" := Json.string)
        ("state" := Json.string)
  in
    "places" := Json.list place



-- SUBSCRIPTIONS


subscriptions : Model -> Sub Msg
subscriptions model =
  Sub.none



-- VIEW


view : Model -> Html Msg
view model =
  div []
    [ div [ centerStyle "row" ]
        [ viewLink Home "Home"
        , viewLink (Blog 42) "Cat Facts"
        , viewLink (Blog 13) "Alligator Jokes"
        , viewLink (Blog 26) "Workout Plan"
        , input
            [ placeholder "Enter a zip code (e.g. 90210)"
            , value model.query
            , onEnter Enter
            , onInput Query
            , style [ ("width", "200px"), ("margin", "0 20px") ]
            ]
            []
        ]
    , hr [] []
    , div [ centerStyle "column" ] (viewPage model)
    ]


viewLink : Page -> String -> Html msg
viewLink page description =
  a [ style [ ("padding", "0 20px") ], href (toHash page) ] [ text description]


viewPage : Model -> List (Html msg)
viewPage model =
  case model.page of
    Home ->
      [ words 60 "Welcome!"
      , text "Play with the links and search bar above. (Press ENTER to trigger the zip code search.)"
      ]

    Blog id ->
      [ words 20 "This is blog post number"
      , words 100 (toString id)
      ]

    Search query ->
      case Dict.get query model.cache of
        Nothing ->
          [ text "..." ]

        Just [] ->
          [ text ("No results found for " ++ query ++ ". Need a valid zip code like 90210.") ]

        Just (location :: _) ->
          [ words 20 ("Zip code " ++ query ++ " is in " ++ location ++ "!") ]


words : Int -> String -> Html msg
words size message =
  span [ style [ ("font-size", toString size ++ "px") ] ] [ text message ]


centerStyle : String -> Attribute msg
centerStyle direction =
  style
    [ ("display", "flex")
    , ("flex-direction", direction)
    , ("align-items", "center")
    , ("justify-content", "center")
    , ("padding", "20px 0")
    ]


onEnter : msg -> Attribute msg
onEnter msg =
  on "keydown" (Json.map (always msg) (Json.customDecoder keyCode is13))


is13 : Int -> Result String ()
is13 code =
  if code == 13 then
    Ok ()

  else
    Err "not the right key code"
