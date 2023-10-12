


$('.training-course-question textarea.form-control').on('input', function () {
    this.style.height = 'auto';
    this.style.height =(this.scrollHeight) + 'px';
});

$("#QuestionType").on("change", function () {
    $(".container-answer").hide();
    $(".container-answer[data-id=" + $(this).val() + "]").show();

    if ($(this).val() == $("#hfOptionalDescriptive").val())
        $(".container-answer").show();
});

$("input[name=rdQuestion]").on("click", function () {
    $("input[name=rdQuestion]").val(0);
    $(this).val(1);
    $("#hfCorrectAnswer").val($(this).attr("data-id"));
});
$(document).ready(function () {
    $("#QuestionType").trigger("change");
});