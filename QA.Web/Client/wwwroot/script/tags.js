window.setAsTagsInput = function (elementId, tagsList) {
    var tags = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.whitespace,
        queryTokenizer: Bloodhound.tokenizers.whitespace,        
        local: tagsList
    });

    //tags.initialize();

    $('#' + elementId).tagsinput({              
        typeaheadjs: {
            name: 'tags',            
            source: tags            
        }
    });
}

window.getTags = function (elementId) {
    return $('#' + elementId).val();
}