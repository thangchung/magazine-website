cd ./tests/unit;
elm-package install -y
elm-make ./TestRunner.elm --output ./tests.js;
node ./tests.js;
