// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener("DOMContentLoaded", function () {
    const sidebar = document.getElementById("sidebar");
    const toggleSidebarBtn = document.getElementById("toggleSidebar");
    const mainContent = document.getElementById("mainContent");

    function toggleSidebar() {
        sidebar.classList.toggle("closed");
        mainContent.classList.toggle("full-width");
    }

    function handleResize() {
        if (window.innerWidth <= 1158) {
            sidebar.classList.add("closed");
            mainContent.classList.add("full-width");
        } else {
            sidebar.classList.remove("closed");
            mainContent.classList.remove("full-width");
        }
    }

    toggleSidebarBtn.addEventListener("click", toggleSidebar);
    window.addEventListener("resize", handleResize);

    // Initial check
    handleResize();

    // Set active classes for navigation
    const navLinks = document.querySelectorAll(".nav-link");
    navLinks.forEach((link) => {
        link.addEventListener("click", () => {
            navLinks.forEach((item) => item.classList.remove("active"));
            link.classList.add("active");
        });
    });
});

document.getElementById('repeatTask').addEventListener('change', function () {
    const repeatsSection = document.getElementById('repeatsSection');
    if (this.checked) {
        repeatsSection.style.display = 'flex';  // Show the section when checkbox is checked
    } else {
        repeatsSection.style.display = 'none';  // Hide the section when checkbox is unchecked
    }
});


//********************************Check box js **************************************

// Get the elements
const isVaultCheckbox = document.getElementById('isVault');
const isVaultFinalContainer = document.getElementById('isVaultFinalContainer');
const VaultLocationContainer = document.getElementById('vaultLocationContainer');

// Function to toggle the visibility of isVaultFinal
function toggleIsVaultFinal() {
    if (isVaultCheckbox.checked) {
        isVaultFinalContainer.style.display = 'block';
        VaultLocationContainer.style.display = 'block';
        
    } else {
        isVaultFinalContainer.style.display = 'none';
        VaultLocationContainer.style.display = 'none';
    }
}

// Initial check on page load
toggleIsVaultFinal();

// Add event listener to the isVault checkbox
isVaultCheckbox.addEventListener('change', toggleIsVaultFinal);


//************************************************************************************************



//*********************************** MOdal popup clicked js ***************************************
    $(document).ready(function () {
        // Open modal when button is clicked
        var TaskCollection = [];
    $("[id*=btnAddTsk]").click(function () {
        $('#myModal').modal('show');
        });

})

//*************************************************************************************************


//******************************************group tab js **************************************



//***********************************************************************************************