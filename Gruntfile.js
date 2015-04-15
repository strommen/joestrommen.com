// Thanks to http://stackoverflow.com/questions/13358680/how-to-config-grunt-js-to-minify-files-separately

module.exports = function (grunt) {
	grunt.initConfig({
		uglify: {
			build: {			
				options: {
					sourceMap: true
				},
                files: [{
                    expand: true,
                    cwd: 'joestrommen.com/scripts/',
                    src: ['*.js', '!*.min.js', '!_references.js', '!3rd-party/'],
                    dest: 'joestrommen.com/scripts/',
					ext: '.min.js',
					extDot: 'first'
                }]
			}
		}
	});

	grunt.loadNpmTasks('grunt-contrib-uglify');

	grunt.registerTask('default', ['uglify']);
};