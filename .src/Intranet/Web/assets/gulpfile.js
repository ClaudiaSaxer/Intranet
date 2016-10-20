'use strict';


var gulp = require('gulp');
var sass = require('gulp-sass');
var ts = require('gulp-typescript');
var rimraf = require('rimraf');
var concat = require('gulp-concat');
var cssmin = require('gulp-cssmin');
var uglify = require('gulp-uglify');
var clean = require('gulp-clean');
var minify = require('gulp-minify');
var cleanCSS = require('gulp-clean-css');
var rename = require('gulp-rename');


var tsPath = './ts';
var tsconfigPath = tsPath + '/tsconfig.json';
var sassPath = './sass';
var jsPath = '../resources/js/intigena';
var cssPath = '../resources/css/intigena';

// Delete the dist directory
gulp.task('clean',
    function() {
        return gulp.src([jsPath,cssPath])
            .pipe(clean({ force: true }));
    });


var tsProject = ts.createProject(tsconfigPath,
{
    typescript: require('typescript')
});

gulp.task('ts',
    function() {

        var tsResult = gulp.src(tsPath+'/*.ts')
            .pipe(ts(tsProject), undefined, ts.reporter.fullReporter());
        return tsResult.js.pipe(gulp.dest(jsPath));
    });

gulp.task('sass',
    function() {
        return gulp.src([sassPath+'/**/*.scss', '!'+sassPath+'/bootstrap'])
            .pipe(sass().on('error', sass.logError))
            .pipe(gulp.dest(cssPath));
    });


gulp.task('minify-js',
    ['ts'],
    function() {
        gulp.src([jsPath+'/**/*.js', '!'+jsPath+'**/*min.js'])
            .pipe(minify({
                ext: {
                    src: '.js',
                    min: '.min.js'
                },
                exclude: ['tasks'],
                ignoreFiles: ['.combo.js', '.min.js']
            }))
            .pipe(gulp.dest(jsPath));
    });

gulp.task('minify-css',
    ['sass'],
    function() {
        return gulp.src([
                cssPath+'/*.css', '!'+cssPath+'/*.min.css'
            ])
            .pipe(cleanCSS({ compatibility: 'ie8' }))
            .pipe(rename({
                suffix: '.min'
            }))
            .pipe(gulp.dest(cssPath));
    });

gulp.task('watch.sass',
    ['sass'],
    function() {
        gulp.watch(sassPath+'/**.scss', ['sass']);
    });
gulp.task('watch.ts',
    ['ts'],
    function() {
        return gulp.watch(tsPath+'/*.ts', ['ts']);
    });
gulp.task('watch', ['watch.ts']);
gulp.task('watch', ['watch.sass']);

gulp.task('default', ['ts', 'sass', 'minify-js', 'minify-css', 'watch']);