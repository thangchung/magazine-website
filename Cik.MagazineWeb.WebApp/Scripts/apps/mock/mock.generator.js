define('mock/mock.generator',
    ['jquery', 'moment'],
    function($, moment) {
        var
            init = function () {
                $.mockJSON.random = true;
                $.mockJSON.log = false;
                $.mockJSON.data.SPEAKER_FIRST_NAME = ['John', 'Dan', 'Scott', 'Hans', 'Ward', 'Jim', 'Ryan', 'Steve', 'Ella', 'Landon', 'Haley', 'Madelyn'];
                $.mockJSON.data.SPEAKER_LAST_NAME = ['Papa', 'Wahlin', 'Guthrie', 'Fjällemark', 'Bell', 'Cowart', 'Niemeyer', 'Sanderson'];
                $.mockJSON.data.IMAGE_SOURCE = ['john_papa.jpg', 'dan_wahlin.jpg', 'scott_guthrie.jpg', 'hans_fjallemark.jpg', 'ward_bell.jpg', 'jim_cowart.jpg', 'ryan_niemeyer.jpg', 'steve_sanderson.jpg'];
                $.mockJSON.data.DATE_TODAY = [moment().format('MMMM DD YYYY')];
                $.mockJSON.data.DATE_FULL = [new Date()];
                $.mockJSON.data.TAG = ['JavaScript', 'Knockout', 'MVVM', 'HTML5', 'Keynote', 'SQL', 'CSS', 'Metro', 'UX'];
                $.mockJSON.data.TRACK = ['Windows 8', 'JavaScript', 'ASP.NET', '.NET', 'Data', 'Mobile', 'Cloud', 'Practices', 'Design'];
                $.mockJSON.data.TITLE = [
                    'Building HTML/JavaScript Apps with Knockout and MVVM',
                    'JsRender Fundamentals',
                    'Introduction to Building Windows 8 Metro Applications',
                    'Building ASP.NET MVC Apps with EF Code First, HTML5, and jQuery',
                    'jQuery Fundamentals',
                    'jQuery Tips and Tricks',
                    'JavaScript for .NET Developers',
                    'jQuery Mobile',
                    'Bootstrap',
                    'Responsive Web Design',
                    'Structuring JavaScript Code',
                    'Keynote'
                ];
                $.mockJSON.data.LEVEL = ["Beginner", "Intermediate", "Advanced"];
                $.mockJSON.data.TWITTER = ['john_papa', 'danwahlin', 'ifthenelse', 'scottgu', 'wardbell'];
                $.mockJSON.data.URL = ['http://www.johnpapa.net', 'http://www.pluralsight.com'];
                $.mockJSON.data.GENDER = ['F', 'M'];
                $.mockJSON.data.RATING = [1, 2, 3, 4, 5];
            },

            generateAttendance = function () {
                var data = $.mockJSON.generateFromTemplate({
                    'attendance|8-16': [{
                        'personId': 1,
                        'sessionId|+1': 1,
                        rating: '@RATING',
                        text: '@LOREM_IPSUM'
                    }]
                });
                // Hard code first one to SPA
                data.attendance[0].personId = 3;
                data.attendance[0].sessionId = 1;
                return data;
            },

            generateRooms = function () {
                var data = $.mockJSON.generateFromTemplate({
                    'rooms|10-20': [{
                        'id|+1': 1,
                        name: '@LOREM'
                    }]
                });
                return data;
            },

            generateSessions = function () {
                var data = $.mockJSON.generateFromTemplate({
                    'sessions|100-120': [{
                        'id|+1': 1,
                        title: '@TITLE',
                        code: '@LOREM',
                        'speakerId|1-50': 1,
                        'trackId|1-10': 1,
                        'timeSlotId|1-15': 1,
                        'roomId|1-10': 1,
                        level: '@LEVEL',
                        'tags|1-5': '@TAG ,',
                        description: '@LOREM_IPSUM'
                    }]
                });
                // Hard code first one to SPA
                data.sessions[0].id = 1;
                data.sessions[0].title = 'Single Page Apps';
                data.sessions[0].speakerId = 3;
                return data;
            },

            generatePersons = function () {
                var data = $.mockJSON.generateFromTemplate({
                    'persons|50-60': [{
                        'id|+1': 1,
                        firstName: '@SPEAKER_FIRST_NAME',
                        lastName: '@SPEAKER_LAST_NAME',
                        email: '@EMAIL',
                        blog: '@URL',
                        twitter: 'http://twitter.com/@' + '@TWITTER',
                        gender: '@GENDER',
                        imageSource: '@IMAGE_SOURCE',
                        bio: '@LOREM_IPSUM'
                    }]
                });
                // Hard code 3rd one to John Papa
                data.persons[2].id = 3;
                data.persons[2].firstName = 'John';
                data.persons[2].lastName = 'Papa';
                data.persons[2].email = 'john@constoso.com';
                return data;
            },

            generateTimeslots = function () {
                var data = $.mockJSON.generateFromTemplate({
                    'timeslots|15-20': [{
                        'id|+1': 1,
                        start: '@DATE_FULL',
                        duration: 60
                    }]
                });
                return data;
            },

            generateTracks = function () {
                var data = $.mockJSON.generateFromTemplate({
                    'tracks|10-15': [{
                        'id|+1': 1,
                        name: '@LOREM'
                    }]
                });
                return data;
            };

            init();
        
        return {
            model: {
                generateAttendance: generateAttendance,
                generateRooms: generateRooms,
                generateSessions: generateSessions,
                generatePersons: generatePersons,
                generateTimeslots: generateTimeslots,
                generateTracks: generateTracks
            }
        };
    });
