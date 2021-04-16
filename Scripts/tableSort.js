function tableSort() {
    $('#quizTable').DataTable({
        paging: true,
        searching: false,
        info: false,
        lengthChange: true,
        language: {
            lengthMenu: "Pokaż _MENU_ rekordów",
            paginate: {
                "previous": "wstecz",
                "next": "dalej"
            }
        }
    });

    $('#examTable').DataTable({
        paging: true,
        searching: false,
        info: false,
        lengthChange: true,
        language: {
            lengthMenu: "Pokaż _MENU_ rekordów",
            paginate: {
                "previous": "wstecz",
                "next": "dalej"
            }
        }
    });
}

window.onload = tableSort;