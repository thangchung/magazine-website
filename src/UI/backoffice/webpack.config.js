var path = require("path");
var CleanWebpackPlugin = require('clean-webpack-plugin');
var bourbon = require('node-bourbon').includePaths;
var neat = require('node-neat').includePaths;

module.exports = {
  entry: {
    app: [
      './src/index.js',
    ],
  },

  output: {
    path: path.resolve(__dirname + '/dist'),
    filename: '[name].js'
  },

  module: {
    loaders: [
      {
        test: /\.html$/,
        exclude: /node_modules/,
        loader: 'file?name=[name].[ext]',
      },
      {
        test: /\.elm$/,
        exclude: [/elm-stuff/, /node_modules/,],
        loader: 'elm-hot!elm-webpack?warn=true',
      },
      {
        test: /\.sass$/,
        loaders: ['style', 'css', 'sass']
      }
    ],

    noParse: /\.elm$/,
  },

  devServer: {
    inline: true,
    stats: {
      colors: true,
      chunks: false,
    },
    proxy: {
      '*': {
        target: 'http://localhost:3000/',
        secure: false,
        changeOrigin: true,
      }
    }
  },

  sassLoader: {
    includePaths: bourbon.concat(neat)
  },

  plugins: [
    new CleanWebpackPlugin('elm-stuff/build-artifacts/0.17.1/user'),
  ]

};
