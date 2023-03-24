
const form = document.querySelector('#stepOneForm');
const alertContainer = document.querySelector('.containing-alert');

form.addEventListener("submit",function(e) {
    e.preventDefault();
    alertContainer.innerHTML = '';
    var selects = document.querySelectorAll("select");
    if (selects[0].value == 0 & selects[1].value == 0 & selects[2].value == 0) {
        alertContainer.insertAdjacentHTML('afterbegin',
            `<div class="alert alert-danger text-center" role="alert"> يرجى تحديد عدد المقاعد </div>`)
        return false
    } else {
        $(this).unbind('submit').submit()
    }

})


