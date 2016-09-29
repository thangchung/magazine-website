module Commands exposing (..)

import Messages exposing (Msg(..))
import Routing exposing (Route(..))


fetchForRoute : Route -> Cmd Msg
fetchForRoute route =
    case route of
        DashboardRoute ->
            Cmd.none

        NotFoundRoute ->
            Cmd.none
