module View exposing (..)

import Html exposing (..)
import Html.App
import Messages exposing (Msg(..))
import Models exposing (Model)
import NavBar
import Routing exposing (Route(..))


view : Model -> Html Msg
view model =
    div []
        [ Html.App.map NavBarMessage <| NavBar.view model.route
        , node "content" [] [ page model ]
        ]


page : Model -> Html Msg
page model =
    case model.route of
        DashboardRoute ->
            dashboard

        NotFoundRoute ->
            notFound


notFound : Html Msg
notFound =
    div [] [ h1 [] [ text "404 - Page not found." ] ]


dashboard : Html Msg
dashboard =
    div []
        [ h1 [] [ text "Dashboard" ] ]
