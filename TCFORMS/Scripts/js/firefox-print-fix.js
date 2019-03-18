(function () {
    'use strict';

    if (navigator.userAgent.search("Firefox") >= 0) {
        var replaceTag = function (element, replaceWith) {
            var outer = element.outerHTML;

            // Replace opening tag
            var regex = new RegExp('^<' + element.tagName, 'ig');
            var newTag = outer.replace(regex, '<' + replaceWith);

            // Replace closing tag
            regex = new RegExp('</' + element.tagName + '>$', 'ig');
            newTag = newTag.replace(regex, '</' + replaceWith + '>');

            $(element).replaceWith(newTag);
        }

        $(window).bind('beforeprint', function () {
            $('fieldset').each(function () {
                this.classList.add('fieldset-fix');
                replaceTag(this, 'div');
            });
        });
        $(window).bind('afterprint', function () {
            $('.fieldset-fix').each(function () {
                replaceTag(this, 'fieldset');
            });
        });
    }
}());