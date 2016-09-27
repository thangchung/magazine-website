# URL Parser

This library helps you turn URLs into nicely structured data.

It is designed to be used with `elm-lang/navigation` to help folks create single-page applications (SPAs) where you manage browser navigation yourself.

> **Note:** This library is meant to serve as a baseline for future URL parsers. For example, it does not handle query parameters and hashes right now. It is more to (1) get folks started using URL parsers and (2) help us gather data on exactly which scenarios people face.


## Examples

Here is a parser that handles URLs like `/blog/42/whale-songs` where `42` can be any integer and `whale-songs` can be any string:

```elm
blog : Parser (Int -> String -> a) a
blog =
  s "blog" </> int </> string
```

Here is a slightly fancier example. This parser turns URLs like `/blog/42` and `/search/badger` into a nice structure that is easier to work with in Elm:

```elm
type DesiredPage = Blog Int | Search String

desiredPage : Parser (DesiredPage -> a) a
desiredPage =
  oneOf
    [ format Blog (s "blog" </> int)
    , format Search (s "search" </> string)
    ]
```

To see an example of this library paired with `elm-lang/navigation`, check out the `examples/` directory of this GitHub repo.


## Background

I first saw this general idea in Chris Done&rsquo;s [formatting][] library. Based on that, Noah and I outlined the API you see in this library. Noah then found Rudi Grinberg&rsquo;s [post][] about type safe routing in OCaml. It was exactly what we were going for. We had even used the names `s` and `(</>)` in our draft API! In the end, we ended up using the &ldquo;final encoding&rdquo; of the EDSL that had been left as an exercise for the reader. Very fun to work through! 😃

[formatting]: http://chrisdone.com/posts/formatting
[post]: http://rgrinberg.com/blog/2014/12/13/primitive-type-safe-routing/
