"use strict";

var gulp = require("gulp"),
    merge = require("merge-stream"),
    rimraf = require("rimraf"),
    less = require('gulp-less'),
    cssmin = require('gulp-cssmin'),
    rename = require('gulp-rename'),
    bundleconfig = require("./package-mapping-config.js");

var paths = {
    webroot: "./wwwroot/"
};

paths.less = [paths.webroot + "**/*.less"];

var regex = {
    css: /\.css$/,
    js: /\.js$/
};

gulp.task('copy:node_modules', function () {
    rimraf.sync(bundleconfig.libsFolder + '/**/*', { force: true });
    var tasks = [];

    for (var mapping in bundleconfig.mappings) {
        if (bundleconfig.mappings.hasOwnProperty(mapping)) {
            var destination = bundleconfig.libsFolder + '/' + bundleconfig.mappings[mapping];
            if (mapping.match(/[^/]+(css|js)$/)) {
                tasks.push(
                    gulp.src(mapping).pipe(gulp.dest(destination))
                );
            } else {
                tasks.push(
                    gulp.src(mapping + '/**/*').pipe(gulp.dest(destination))
                );
            }
        }
    }

    return merge(tasks);
});

gulp.task('minify:less', function () {

    return gulp.src(paths.less)
        .pipe(less().on('error', function (err) {
            console.log("less()");
            console.log(err);
        }))
        .pipe(gulp.dest(paths.webroot))
        .pipe(cssmin().on('error', function(err) {
            console.log("cssmin()");
            console.log(err);
        }))
        .pipe(rename({suffix: '.min'}))
        .pipe(gulp.dest(paths.webroot));
});

function getBundles(regexPattern) {
    return bundleconfig.bundles.filter(function (bundle) {
        return regexPattern.test(bundle.outputFileName);
    });
}

function getOutputFileName(fullFilePath) {
    var lastIndexOfSlash = fullFilePath.lastIndexOf('/');
    return fullFilePath.substr(lastIndexOfSlash, fullFilePath.length - lastIndexOfSlash);
}

function getOutputFolder(fullFilePath) {
    var lastIndexOfSlash = fullFilePath.lastIndexOf('/');
    return fullFilePath.substr(0, lastIndexOfSlash);
}