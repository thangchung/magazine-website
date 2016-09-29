module Update exposing (..)

import Messages exposing (Msg(..))
import Models exposing (Model)
import NavBar


update : Msg -> Model -> ( Model, Cmd Msg )
update msg model =
    case msg of
        NavBarMessage subMsg ->
            NavBar.update subMsg model
                |> (\( m, c ) -> ( m, Cmd.map NavBarMessage c ))
