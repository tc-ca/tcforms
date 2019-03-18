(function () {
    'use strict';

    var pageUrl = [location.protocol, '//', location.host, location.pathname].join('');
    $('nav a.list-group-item').each(function () {
        var url = this.href.split('?')[0];
        if (url === pageUrl) {
            this.classList.add('wb-navcurr');
        }
    })
}());
