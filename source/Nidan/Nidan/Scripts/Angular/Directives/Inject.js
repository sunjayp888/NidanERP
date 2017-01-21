(function() {
    'use strict';

    angular
        .module('Nidan')
        .directive('inject', inject);

    function inject () {
        // Usage:
        //     <inject></inject>
        // Creates:
        // 
        var directive = {
            link: link,
            restrict: 'A'
        };

        return directive;

        function link($scope, $element, $attrs, controller, $transclude) {
            if (!$transclude) {
                throw minErr('ngTransclude')('orphan',
                 'Illegal use of ngTransclude directive in the template! ' +
                 'No parent directive that requires a transclusion found. ' +
                 'Element: {0}',
                 startingTag($element));
            }
            var innerScope = $scope.$new();
            var targetScope = $element.inheritedData('transscope');

            if (targetScope) {
                innerScope.model = targetScope.model;
            }

            $transclude(innerScope, function (clone) {
                $element.empty();
                $element.append(clone);
                $element.on('$destroy', function () {
                    innerScope.$destroy();
                });
            });
            
        }
    }

})();