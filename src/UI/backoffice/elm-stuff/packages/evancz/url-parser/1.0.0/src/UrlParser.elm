module UrlParser exposing
  ( Parser
  , s
  , string, int
  , (</>)
  , oneOf, format, custom
  , parse
  )
  -- where

{-| This library helps you turn URLs into nicely structured data.

It is designed to be used with `elm-lang/navigation` to help folks create
single-page applications (SPAs) where you manage browser navigation yourself.

# Parsers
@docs Parser, s, string, int

# Combining Parsers
@docs (</>), oneOf, format, custom

# Run Parsers
@docs parse

-}

import String



-- PARSERS


{-| A `Parser` is a way of turning a URL like `/blog/42/cat-herding-techniques`
into structured data.

The two type variables can be a bit tricky to understand. I think the best way
to proceed is to just start using it. You can go far if you just assume it will
do the intuitive thing.

**Note:** If you *insist* on digging deeper, I recommend figuring out the type
of `int </> int` based on the type signatures for `int` and `</>`. You may be
able to just know based on intuition, but instead, you should figure out
exactly how every type variable gets unified. It is pretty cool! From there,
maybe check out the implementation a bit.
-}
type Parser formatter result =
  Parser (Chunks -> formatter -> Result String (Chunks, result))


type alias Chunks =
  { seen : List String
  , rest : List String
  }


{-| Actually run a parser. For example, if we want to handle blog posts with
an ID number and a name, we might write something like this:

    blog : Parser (Int -> String -> a) a
    blog =
      s "blog" </> int </> string

    result : Result String (Int, String)
    result =
      parse (,) blog "/blog/42/cat-herding-techniques"

    -- result == OK (42, "cat-herding-techniques")

Notice that we use the `(,)` function for building tuples as the first argument
to `parse`. The `blog` parser requires a formatter of type `(Int -> String -> a)`
so we need to provide that to actually run things.

**Note:** The error messages are intended to be fairly helpful. They are
nice for debugging during development, but probably too detailed to show
directly to users.
-}
parse : formatter -> Parser formatter a -> String -> Result String a
parse input (Parser actuallyParse) url =
  case actuallyParse (Chunks [] (String.split "/" url)) input of
    Err msg ->
      Err msg

    Ok ({rest}, result) ->
      case rest of
        [] ->
          Ok result

        [""] ->
          Ok result

        _ ->
          Err <|
            "The parser worked, but /"
            ++ String.join "/" rest ++ " was left over."



-- PARSE SEGMENTS


{-| A parser that matches *exactly* the given string. So the following parser
will match the URL `/hello/world` and nothing else:

    helloWorld : Parser a a
    helloWorld =
      s "hello" </> s "world"
-}
s : String -> Parser a a
s str =
  Parser <| \{seen,rest} result ->
    case rest of
      [] ->
        Err ("Got to the end of the URL but wanted /" ++ str)

      chunk :: remaining ->
        if chunk == str then
          Ok ( Chunks (chunk :: seen) remaining, result )

        else
          Err ("Wanted /" ++ str ++ " but got /" ++ String.join "/" rest)


{-| A parser that matches any string. So the following parser will match
URLs like `/search/whatever` where `whatever` can be replaced by any string
you can imagine.

    search : Parser (String -> a) a
    search =
      s "search" </> string

**Note:** this parser will only match URLs with exactly two segments. So things
like `/search/this/that` would fail. You could use `search </> string` to handle
that case if you wanted though!
-}
string : Parser (String -> a) a
string =
  custom "STRING" Ok


{-| A parser that matches any integer. So the following parser will match
URLs like `/blog/42` where `42` can be replaced by any positive number.

    blog : Parser (Int -> a) a
    blog =
      s "blog" </> int

**Note:** this parser will only match URLs with exactly two segments. So things
like `/blog/42/cat-herding-techniques` would fail. You could use `blog </> string`
to handle that scenario if you wanted though!
-}

int : Parser (Int -> a) a
int =
  custom "NUMBER" String.toInt


{-| Create a custom segment parser. The `int` and `string` parsers are actually
defined with it like this:

    import String

    string : Parser (String -> a) a
    string =
      custom "STRING" Ok

    int : Parser (Int -> a) a
    int =
      custom "NUMBER" String.toInt

The first argument is to help with error messages. It lets us say something
like, &ldquo;Got to the end of the URL but wanted /STRING&rdquo; instead of
something totally nonspecific. The second argument lets you process the URL
segment however you want.

An example usage would be a parser that only accepts segments with a particular
file extension. So stuff like this:

    css : Parser (String -> a) a
    css =
      custom "FILE.css" <| \str ->
        if String.endsWith ".css" str then
          Ok str

        else
          Err "Need something that ends with .css"
-}
custom : String -> (String -> Result String a) -> Parser (a -> output) output
custom tipe stringToSomething =
  Parser <| \{seen,rest} func ->
    case rest of
      [] ->
        Err ("Got to the end of the URL but wanted /" ++ tipe)

      chunk :: remaining ->
        case stringToSomething chunk of
          Ok something ->
            Ok ( Chunks (chunk :: seen) remaining, func something )

          Err msg ->
            Err ("Parsing `" ++ chunk ++ "` went wrong: " ++ msg)



-- COMBINING PARSERS


infixr 8 </>


{-| Combine parsers. It can be used to combine very simple building blocks
like this:

    hello : Parser (String -> a) a
    hello =
      s "hello" </> string

So we can say hello to whoever we want. It can also be used to put together
arbitrarily complex parsers, so you *could* say something like this too:

    doubleHello : Parser (String -> String -> a) a
    doubleHello =
      hello </> hello

This would match URLs like `/hello/alice/hello/bob`. The point is more that you
can build complex URL parsers in submodules and then put them on the end of
parsers in parent modules.
-}
(</>) : Parser a b -> Parser b c -> Parser a c
(</>) (Parser parseFirst) (Parser parseRest) =
  Parser <| \chunks func ->
    parseFirst chunks func
      `Result.andThen` \(nextChunks, nextFunc) ->

    parseRest nextChunks nextFunc


{-| Try a bunch of parsers one at a time. This is useful when there is a known
set of branches that are possible. For example, maybe we have a website that
just has a blog and a search:

    type DesiredPage = Blog Int | Search String

    desiredPage : Parser (DesiredPage -> a) a
    desiredPage =
      oneOf
        [ format Blog (s "blog" </> int)
        , format Search (s "search" </> string)
        ]

The `desiredPage` parser will first try to match things like `/blog/42` and if
that fails it will try to match things like `/search/badgers`. It fails if none
of the parsers succeed.
-}
oneOf : List (Parser a b) -> Parser a b
oneOf choices =
  Parser (oneOfHelp choices)


oneOfHelp : List (Parser a b) -> Chunks -> a -> Result String (Chunks, b)
oneOfHelp choices chunks formatter =
  case choices of
    [] ->
      Err "Tried many parsers, but none of them worked!"

    Parser parser :: otherParsers ->
      case parser chunks formatter of
        Err _ ->
          oneOfHelp otherParsers chunks formatter

        Ok answerPair ->
          Ok answerPair


{-| Customize an existing parser. Perhaps you want a parser that matches any
string, but gives you the result with all lower-case letters:

    import String

    caseInsensitiveString : Parser (String -> a) a
    caseInsensitiveString =
      format String.toLower string

    -- String.toLower : String -> String
    -- string : Parser (String -> a) a

I recommend working through how the type variables in `format` would get
unified to get a better idea of things, but an intuition of how to use things
is probably enough.
-}
format : formatter -> Parser formatter a -> Parser (a -> result) result
format input (Parser parse) =
  Parser <| \chunks func ->
    case parse chunks input of
      Err msg ->
        Err msg

      Ok (newChunks, value) ->
        Ok (newChunks, func value)
