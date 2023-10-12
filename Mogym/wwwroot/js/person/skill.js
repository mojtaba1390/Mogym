var counterSkill = 0;
$(document).ready(function () {
    counterSkill = $("#ProfessionalSkillPersonCount").val() - 1;

    if (counterSkill == -1) {
        AddSkill()
    }
});

function AddSkill() {
    counterSkill++;
    $.ajax({
        type: "POST",
        url: "/Person/AddSkill",
        data: { "countSkill": counterSkill },
        success: function (response) {
            $("#InputHidden").append('<input type="hidden" id="professionalSkillPersonDTOs_' + counterSkill +
                '__Id" name ="professionalSkillPersonDTOs[' + counterSkill + '].Id" class="form-control" value = "0" />');
            $("#professionalSkill").append(response);
            if ($('table tr.attr').length <= 1) {
                $("table .attr .remove").hide();

            } else {
                $("table .attr .remove").show();
            }
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}


$(function () {
   
    $('#skill-form').on('submit', function () {
        $('input[name^=professionalSkillPersonDTOs]').each(function () {
            $(this).rules("add", {
                required: true,
                messages: {
                    required: "پر کردن این فیلد اجباری است"
                }
            })
        })
        $('select[name^=professionalSkillPersonDTOs]').each(function () {
            $(this).rules("add", {
                required: true,
                messages: {
                    required: "پر کردن این فیلد اجباری است"
                }
            })
        })

    })

    $('#skill-form').validate();
})