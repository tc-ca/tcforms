(function () {
    'use strict';

    var labels = {
        unsavedChanges: {
            en: "You have unsaved changes!",
            fr: "Vous avez des changements non enregistrés!"
        }
    };

    var lang = $('#wb-lng > ul > li > a').attr('lang') === 'fr' ? 'en' : 'fr';

    // always trim text and textarea to avoid errors
    // TODO: Improve the handling of 2000 varchar2; maybe clob opt?
    $(function () {
        $("textarea[maxlength],input[type=text]").bind('input propertychange', function () {
            var maxLength = $(this).attr('maxlength');

            $(this).val($(this).val().replace(/\n/g, "\r\n").substring(0, maxLength));

        })
    });


    // Track changes to all forms
    $('form').areYouSure({
        // this message is for older browsers only. Newer browsers ignore custom messages and display the browser default message.
        message: labels.unsavedChanges[lang]
    });

    window.TcForms = {
        LoadFields: function (element, fields) {
            // Add hidden fields for checkbox lists since unchecked checkboxes aren't posted
            var hiddenFields = fields.filter(function (field) {
                return field.type === 'checkbox-group';
            }).map(function (field) {
                return {
                    type: 'hidden',
                    name: field.name + '.hidden'
                };
            });
            fields = hiddenFields.concat(fields);

            $(element).formRender({ dataType: 'json', formData: fields });

            // remove empty checkbox labels since browsers don't supprt :blank css selector yet
            $('label.fb-checkbox-group-label').filter(function () {
                return $(this).text().trim() === '';
            }).remove();

            // Rescan the forms to account for newly added fields
            $('form').trigger('rescan.areYouSure');
        },
        GenerateSections: function (element, sections) {
            $(element).find('.section-render').each(function () {
                var element = $(this);
                var id = element.data('section');
                var fields = sections.filter(function (s) {
                    return s.sectionId === id;
                })[0].fields;
                window.TcForms.LoadFields(element, fields);
            });
        }
    };
}());
