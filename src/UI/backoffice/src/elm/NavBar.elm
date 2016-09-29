module NavBar exposing (..)

import Dict
import Html exposing (..)
import Html.Attributes exposing (..)
import Navigation
import Routing exposing (Route(..))
import Utils exposing (onClickNoDefault)


type Msg
    = Dashboard


update : Msg -> a -> ( a, Cmd b )
update msg model =
    case msg of
        Dashboard ->
            ( model, Navigation.newUrl "#dashboard" )


msgToRoute : Msg -> Route
msgToRoute msg =
    case msg of
        Dashboard ->
            DashboardRoute


view : Route -> Html Msg
view currentRoute =
    let
        activeClass msg =
            if isActive msg then
                [ class "active" ]
            else
                []

        isActive msg =
            currentRoute == (msgToRoute msg) || isChildRoute msg

        isChildRoute msg =
            Dict.get (toString msg) childRoutes
                |> Maybe.map (\rs -> List.member currentRoute rs)
                |> Maybe.withDefault False

        childRoutes =
            [ ( Dashboard, [] )
            ]
                |> List.map (\( msg, ms ) -> ( toString msg, List.map msgToRoute ms ))
                |> Dict.fromList

        nav =
            node "nav" [ class "navbar navbar-default" ]

        rootItem msg url name subItems =
            li (activeClass msg)
                [ navLink msg url name
                , ul [ class "submenu" ] subItems
                ]

        navLink msg url name =
            a ([ onClickNoDefault msg, href url ] ++ activeClass msg)
                [ text name ]

        emptyItem name =
            li [] [ a [] [ text name ] ]

        navHeader =
            div [ class "navbar-header" ]
                [ button
                    [ type' "button"
                    , class "navbar-toggle collapsed"
                    , attribute "data-target" "#bs-example-navbar-collapse-1"
                    , attribute "data-toggle" "collapse"
                    , attribute "aria-expanded" "false"
                    ]
                    [ span [ class "sr-only" ] [ text "Toggle navigation" ]
                    , span [ class "icon-bar" ] []
                    , span [ class "icon-bar" ] []
                    , span [ class "icon-bar" ] []
                    ]
                , a [ class "navbar-brand", href "#" ] [ text "Magazine Website" ]
                ]
    in
        nav
            [ div [ class "container-fluid" ]
                [ navHeader
                , div [ class "collapse navbar-collapse", id "bs-example-navbar-collapse-1" ]
                    [ ul
                        [ class "nav navbar-nav" ]
                        [ li [ class "active" ]
                            [ navLink Dashboard "#dashboard" "Dashboard"
                            ]
                        ]
                    ]
                ]
            ]
