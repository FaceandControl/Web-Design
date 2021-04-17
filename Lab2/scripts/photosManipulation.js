$(".panel-heading").parent('.panel').hover(
    function() {
      let a = $(".collapse")
      a.collapse('show');
    },
    function() {
      $(".collapse").collapse('hide');
    }
  );

  var defaultThreads = [
    {
        id: 1,
        title: "Thread 1",
        author: "Aaron",
        date: Date.now(),
        content: "Thread content",
        comments: [
            {
                author: "Jack",
                date: Date.now(),
                content: "Hey there"
            },
            {
                author: "Arthur",
                date: Date.now(),
                content: "Hey to you too"
            }
        ]
    },
    {
        id: 2,
        title: "Thread 2",
        author: "Aaron",
        date: Date.now(),
        content: "Thread content 2",
        comments: [
            {
                author: "Jack",
                date: Date.now(),
                content: "Hey there"
            },
            {
                author: "Arthur",
                date: Date.now(),
                content: "Hey to you too"
            }
        ]
    }
]

var threads = defaultThreads
if (localStorage && localStorage.getItem('threads')) {
    threads = JSON.parse(localStorage.getItem('threads'));
} else {
    threads = defaultThreads;
    localStorage.setItem('threads', JSON.stringify(defaultThreads));
}
        

      function addComment(comment) {
          var commentHtml = `
              <div class="comment">
                  <div class="top-comment">
                      <p class="user">
                          ${comment.author}
                      </p>
                      <p class="comment-ts">
                          ${new Date(comment.date).toLocaleString()}
                      </p>
                  </div>
                  <div class="comment-content">
                      ${comment.content}
                  </div>
              </div>
          `
          comments.insertAdjacentHTML('beforeend', commentHtml);
      }

      var comments = document.querySelector('ol');
      for (let comment of thread.comments) {
          addComment(comment);
      }

      var btn = document.querySelector('button');
      btn.addEventListener('click', function() {
          var txt = document.querySelector('textarea');
          var comment = {
              content: txt.value,
              date: Date.now(),
              author: 'Aaron'
          }
          addComment(comment);
          txt.value = '';
          thread.comments.push(comment);
          localStorage.setItem('threads', JSON.stringify(threads));
      })