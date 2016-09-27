module Tests exposing (..)

import Time
import ElmTest exposing (..)
import Http
import HttpBuilder exposing (..)
import Json.Decode as Decode
import Json.Encode as Encode
import Native.Polyfills


polyfillsEnabled : Bool
polyfillsEnabled =
    Native.Polyfills.enabled


toTuple : RequestBuilder -> ( Request, Settings )
toTuple builder =
    ( toRequest builder, toSettings builder )


testDecoder : Decode.Decoder String
testDecoder =
    Decode.at [ "hello" ] Decode.string


initialRequest : Request
initialRequest =
    { verb = "GET"
    , url = "http://example.com"
    , headers = []
    , body = Http.empty
    }


all : Test
all =
    suite "All tests"
        [ test "polyfills are set up" <| assertEqual polyfillsEnabled True
        , suite "Request Building"
            [ get "http://example.com"
                |> withHeader "Test" "Header"
                |> withHeaders [ ( "OtherTest", "Header" ) ]
                |> withStringBody """{ "test": "body" }"""
                |> withTimeout (10 * Time.second)
                |> withMimeType "test/mime-type"
                |> withCredentials
                |> toTuple
                |> assertEqual
                    ( { verb = "GET"
                      , url = "http://example.com"
                      , headers = [ ( "OtherTest", "Header" ), ( "Test", "Header" ) ]
                      , body = Http.string """{ "test": "body" }"""
                      }
                    , { timeout = 10 * Time.second
                      , onStart = Nothing
                      , onProgress = Nothing
                      , desiredResponseType = Just "test/mime-type"
                      , withCredentials = True
                      }
                    )
                |> test "should build request and settings with expected params"
            ]
        , suite "with*Body functions"
            [ get "http://example.com"
                |> withStringBody "hello"
                |> toRequest
                |> .body
                |> assertEqual (Http.string "hello")
                |> test "withStringBody applies string directly"
            , get "http://example.com"
                |> withJsonBody (Encode.string "hello")
                |> toRequest
                |> .body
                |> assertEqual (Http.string "\"hello\"")
                |> test "withJsonBody applies a Json.Value as a string"
            , get "http://example.com"
                |> withUrlEncodedBody [ ( "hello", "world" ), ( "test", "stuff" ) ]
                |> toRequest
                |> .body
                |> assertEqual (Http.string "hello=world&test=stuff")
                |> test "withUrlEncodedBody encodes pairs as url components"
            , get "http://example.com"
                |> withMultipartBody [ Http.stringData "hello" "world" ]
                |> toRequest
                |> .body
                |> assertEqual (Http.multipart [ Http.stringData "hello" "world" ])
                |> test "withMultipartBody passes through to Http.multipart"
            , get "http://example.com"
                |> withMultipartStringBody [ ( "hello", "world" ) ]
                |> toRequest
                |> .body
                |> assertEqual (Http.multipart [ Http.stringData "hello" "world" ])
                |> test "withMultipartStringBody first converts string pairs to string data and then passes to multipart"
            ]
        , suite "BodyReaders"
            [ stringReader (Http.Text "test value")
                |> assertEqual (Ok "test value")
                |> test "should extract a text value as a string"
            , jsonReader testDecoder (Http.Text """{ "hello": "world" }""")
                |> assertEqual (Ok "world")
                |> test "should extract a json value with a decoder"
            ]
        ]
