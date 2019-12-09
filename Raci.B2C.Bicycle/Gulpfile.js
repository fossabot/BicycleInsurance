/// <binding BeforeBuild='less' />
var gulp = require('gulp');
var less = require('gulp-less');
var ts = require('gulp-typescript');
var path = require('path');
var plumber = require('gulp-plumber');
var fs = require("fs");
var merge = require('merge');
var rimraf = require('rimraf');

gulp.task('less', function () {
    return gulp.src('./Content/**/*.less')
    .pipe(plumber())
      .pipe(less({
          paths: [path.join(__dirname, 'less', 'includes')]
      }))
      .pipe(gulp.dest('./content/'));
});

//todo: cleanup/rework gulp tasks for typescript.
eval("var project = " + fs.readFileSync("./package.json"));

var paths = {
    npm: './node_modules/',
    lib: "./" + project.webroot + "./Scripts/",
    tsSource: './*.ts',
    tsOutput: "./" + project.webroot + './scripts/',
    tsDef: "./Scripts/"
};

gulp.task("clean", function (cb) {
    rimraf(paths.tsOutput, cb);
});

var tsProject = ts.createProject({
    declarationFiles: true,
    noExternalResolve: false,
    module: 'AMD',
    removeComments: true
});

gulp.task('ts-compile', function () {
    
    var tsResult = gulp.src(paths.tsSource)
                    .pipe(ts(tsProject));
    return merge([
        tsResult.dts.pipe(gulp.dest(paths.tsDef)),
        tsResult.js.pipe(gulp.dest(paths.tsOutput))
    ]);
});

gulp.task('watch', ['ts-compile'], function () {
    gulp.watch(paths.tsDef, ['ts-compile']);
});

gulp.task("copy", function () {
    var npm = {
        "requirejs": "requirejs/require.js"
    }
    
    for (var destinationDir in npm) {
        gulp.src(paths.npm + npm[destinationDir])
            .pipe(gulp.dest(paths.lib + destinationDir));
    }
});