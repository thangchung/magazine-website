define('mock/mock',
    [
    'mock/mock.generator'
    ],
    function (generator) {
        var
            model = generator.model,
            
            dataserviceInit = function () {
            };

    return {
        dataserviceInit: dataserviceInit    
    };
});