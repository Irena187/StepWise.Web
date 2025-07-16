function removeBookmark(careerPathId) {
    if (confirm('Are you sure you want to remove this bookmark?')) {
        fetch('/Bookmark/Remove', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded',
            },
            body: `careerPathId=${careerPathId}`
        })
            .then(response => {
                if (response.ok) {
                    location.reload();
                } else {
                    alert('Error removing bookmark. Please try again.');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Error removing bookmark. Please try again.');
            });
    }
}