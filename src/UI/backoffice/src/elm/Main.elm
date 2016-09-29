module Main exposing (..)

import Navigation
import Commands exposing (fetchForRoute)
import Messages exposing (Msg)
import Models exposing (Model, initialModel)
import Routing exposing (Route(..))
import Update exposing (update)
import View exposing (view)


init : Result String Route -> ( Model, Cmd Msg )
init result =
    let
        currentRoute =
            Routing.routeFromResult result
    in
        ( initialModel currentRoute, fetchForRoute currentRoute )


urlUpdate : Result String Route -> Model -> ( Model, Cmd Msg )
urlUpdate result model =
    let
        updatedModel =
            { model | route = Routing.routeFromResult result }
    in
        ( updatedModel, fetchForRoute updatedModel.route )


subscriptions : Model -> Sub Msg
subscriptions model =
    Sub.none


main : Program Never
main =
    Navigation.program Routing.parser
        { init = init
        , view = view
        , update = update
        , urlUpdate = urlUpdate
        , subscriptions = subscriptions
        }
