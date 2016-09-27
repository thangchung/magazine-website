module Main exposing (..)

import Html exposing (..)
import Html.Attributes exposing (..)
import Html.App as Html


main =
    Html.beginnerProgram { model = model, view = view, update = update }


type alias Model =
    Int


model : number
model =
    0


type Msg
    = NoOp


update : Msg -> Model -> Model
update msg model =
    case msg of
        NoOp ->
            model


view : Model -> Html Msg
view model =
    p [ id "container" ] [ text "Hello Elm !!!" ]
