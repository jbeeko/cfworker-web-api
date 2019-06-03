const CloudflareWorkerPlugin = require('cloudflare-worker-webpack-plugin');

module.exports = env => {
    var env = env
        ? {deploy: true, CF_EMAIL: env.CF_EMAIL, CF_KEY: env.CF_KEY}
        : {prod: false, CF_EMAIL: "", CF_KEY: ""}

    return {
        entry: {worker: ['./src/Worker.fsproj']},
        target: 'webworker',
        output: {
            path: __dirname,
            filename: '[name].bndl.js',
        },
        mode: "production",
        resolve: {
            // See https://github.com/fable-compiler/Fable/issues/1490
            symlinks: false
        },
        plugins: [
            new CloudflareWorkerPlugin(
                // CF credentials for instructions on where to find them see:
                // https://developers.cloudflare.com/workers/api/
                env.CF_EMAIL,
                env.CF_KEY,
                {   // Options object, for addtional options see:
                    // https://www.npmjs.com/package/cloudflare-worker-webpack-plugin
                    site: `rec-room.io`,
                    enabledPatterns: `rec-room.io/*`,
                    clearRoutes: true,
                    verbose: true,
                    colors: true,
                    enabled: env.prod,
                    script: "worker.bndl.js"
                } 
            )
        ],
        module: {
            rules: [
                // - fable-loader: transforms F# into JS
                {
                    test: /\.fs(x|proj)?$/,
                    use: {
                        loader: "fable-loader",
                        options: {
                            babel: {
                                // More info at https://github.com/babel/babel/blob/master/packages/babel-preset-env/README.md
                                presets: [
                                    ["@babel/preset-env", {
                                        "modules": false,
                                        "useBuiltIns": false,
                                        "loose": true,
                                        // Use babel-preset-env to generate JS compatible with latest Chrome V8
                                        "targets": {
                                            "chrome": "73"
                                        }
                                    }]
                                ]
                            }
                        }
                    },
                }
            ]
        }
    };
}