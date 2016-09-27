module Main exposing (..)

import ElmTest exposing (..)
import Tests


main : Program Never
main =
    runSuite Tests.all
