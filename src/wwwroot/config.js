System.config({
  "baseURL": "/",
  "transpiler": "babel",
  "babelOptions": {
    "optional": [
      "runtime"
    ]
  },
  "paths": {
    "*": "*.js",
    "github:*": "lib/github/*.js",
    "npm:*": "lib/npm/*.js"
  }
});

System.config({
  "map": {
    "babel": "npm:babel-core@5.8.22",
    "babel-runtime": "npm:babel-runtime@5.8.20",
    "core-js": "npm:core-js@0.9.18",
    "hammer": "github:hammerjs/hammer.js@2.0.4",
    "ixisio/bootstrap-touch-carousel": "github:ixisio/bootstrap-touch-carousel@0.8.0",
    "jquery": "github:components/jquery@2.1.4",
    "jquery-validation": "github:jzaefferer/jquery-validation@1.14.0",
    "jquery-validation-unobtrusive": "github:aspnet/jquery-validation-unobtrusive@3.2.2",
    "github:aspnet/jquery-validation-unobtrusive@3.2.2": {
      "jquery-validation": "github:jzaefferer/jquery-validation@1.14.0"
    },
    "github:jspm/nodelibs-process@0.1.1": {
      "process": "npm:process@0.10.1"
    },
    "github:jzaefferer/jquery-validation@1.14.0": {
      "jquery": "github:components/jquery@2.1.4"
    },
    "npm:babel-runtime@5.8.20": {
      "process": "github:jspm/nodelibs-process@0.1.1"
    },
    "npm:core-js@0.9.18": {
      "fs": "github:jspm/nodelibs-fs@0.1.2",
      "process": "github:jspm/nodelibs-process@0.1.1",
      "systemjs-json": "github:systemjs/plugin-json@0.1.0"
    }
  }
});

