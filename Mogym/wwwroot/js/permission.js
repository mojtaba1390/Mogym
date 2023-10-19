

$(document).ready(function () {

    $(function () {
        $('#jstree').on('changed.jstree', function (e, data) {
            var i, j;
            var selectedItems = [];
            debugger;
            for (i = 0, j = data.selected.length; i < j; i++) {

                //Fetch the Id.
                var id = data.selected[i];

                //Remove the ParentId.
                if (id.indexOf('-') != -1) {
                    id = id.split("-")[1];
                }

                //Add the Node to the JSON Array.
                selectedItems.push({
                    text: data.instance.get_node(data.selected[i]).text,
                    id: id,
                    parent: data.selected[i].split("-")[0]
                });
            }

            //Serialize the JSON Array and save in HiddenField.
            $('#selectedItems').val(JSON.stringify(selectedItems));
        }).jstree({
            "core": {
                "themes": {
                    "variant": "large"
                },
                "data": @t
    },
        "checkbox": {
        "keep_selected_style": false
    },
        "plugins": ["wholerow", "checkbox"]
            });
        });


})