'use strict';

var gulp = require('gulp');
var sass = require('gulp-sass');
var ts = require('gulp-typescript');
var merge = require('merge2');  // Requires separate installation

gulp.task('sass', function () {
    return gulp.src('./sass/**/*.scss')
      .pipe(sass().on('error', sass.logError))
      .pipe(gulp.dest('./css'));
});


gulp.task('scripts', function () {
    var tsResult = gulp.src('lib/**/*.ts')
        .pipe(ts({
            declaration: true
        }));

    return merge([
        tsResult.dts.pipe(gulp.dest('release/definitions')),
        tsResult.js.pipe(gulp.dest('release/js'))
    ]);
});

gulp.task('watch', ['scripts'], function () {
    gulp.watch('lib/*.ts', ['scripts']);
    gulp.watch('./sass/**/*.scss', ['sass']);

});