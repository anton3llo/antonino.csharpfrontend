function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

async function sleepTimer() {
    await sleep(30000);
    $.getScript('Scripts/progress_bar.js', function () {
        $.hideprogress();
    });
}