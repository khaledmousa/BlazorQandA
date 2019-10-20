window.setAsTagsInput = function (elementId) {
    var tags = new Bloodhound({
        datumTokenizer: function (tag) {
            console.log(tag);
            return Bloodhound.tokenizers.whitespace(tag.name);
        },
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        prefetch: {
            url: '/api/Tags/',
            filter: function (tags) {
                return $.map(tags, function (tag) {
                    return { id: tag.id, name: tag.name };
                });
            }
        }
    });
    tags.initialize();

    $('#' + elementId).tagsinput({                
        typeaheadjs: {
            name: 'tags',
            display: 'name',                        
            source: tags
        }
    });
}