module Models exposing (..)

import Dict
import Routing


type alias Model =
    { route : Routing.Route
    }


initialModel : Routing.Route -> Model
initialModel route =
    { route = route
    }
