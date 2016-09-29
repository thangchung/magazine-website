module Utils exposing (..)

import Html
import Html.Events exposing (defaultOptions)
import Json.Decode


onClickNoDefault : msg -> Html.Attribute msg
onClickNoDefault msg =
    Html.Events.onWithOptions "click"
        { defaultOptions | preventDefault = True }
        (Json.Decode.succeed msg)
