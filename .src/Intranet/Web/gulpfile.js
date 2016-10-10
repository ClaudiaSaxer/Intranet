'use strict';

    

var gulp = require('gulp');
var sass = require('gulp-sass');
var ts = require('gulp-typescript');
var rimraf = require("rimraf");
var concat = require("gulp-concat");
var cssmin = require("gulp-cssmin");
var uglify = require("gulp-uglify");
var clean = require('gulp-clean');
var minify = require('gulp-minify');

var destPath = './libs/';


// Delete the dist directory
gulp.task('clean', function () {
    return gulp.src(destPath)
        .pipe(clean());
});


var tsProject = ts.createProject('tsScripts/tsconfig.json', {
    typescript: require('typescript')
});
gulp.task('ts', function (done) {

    var tsResult = gulp.src([
            "tsScripts/*.ts"
    ])
        .pipe(ts(tsProject), undefined, ts.reporter.fullReporter());
    return tsResult.js.pipe(gulp.dest('./Scripts/own'));
});

gulp.task('sass',
    function () {
        return gulp.src('./Content/own/**/*.scss')
            .pipe(sass().on('error', sass.logError))
            .pipe(gulp.dest('./Content'));
    });

gulp.task('compress',['ts'], function () {
    gulp.src(['./Scripts/own/**/*.js', '!./Scripts/own/**/*min.js'])
      .pipe(minify({
          ext: {
              src: '.js',
              min: '.min.js'
          },
          exclude: ['tasks'],
          ignoreFiles: ['.combo.js', '.min.js']
      }))
      .pipe(gulp.dest('./Scripts/own'))
});


gulp.task('watch.sass', ['sass'], function () {
    gulp.watch('./sass/**/*.scss', ['sass']);
});
gulp.task('watch.ts', ['ts'], function () {
    return gulp.watch('tsScripts/*.ts', ['ts']);
});
gulp.task('watch', ['watch.ts']);
gulp.task('watch', ['watch.sass']);

gulp.task('default', ['ts', 'compress','sass','watch']);

