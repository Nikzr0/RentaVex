document.addEventListener("DOMContentLoaded", function () {
    var isWarnedCheckbox = document.getElementById("isWarnedCheckbox");
    var warningMessageGroup = document.getElementById("warningMessageGroup");

    // Initial check for IsWarned
    toggleWarningMessage();

    // Attach an event listener to the checkbox
    isWarnedCheckbox.addEventListener("change", toggleWarningMessage);

    function toggleWarningMessage() {
        // Show/hide the WarningMessage field based on the checkbox value
        // x ? 1 : 2 -->> if x is true then 1 otherwise 2
        warningMessageGroup.style.display = isWarnedCheckbox.checked ? "block" : "none";
    }
});
