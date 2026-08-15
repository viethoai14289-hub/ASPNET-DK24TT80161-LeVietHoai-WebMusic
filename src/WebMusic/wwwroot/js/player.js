// player.js — thanh player cố định + localStorage, phát liên tục qua trang
(function () {
    const KEY = 'webmusic_player';

    function load() {
        try { return JSON.parse(localStorage.getItem(KEY)) || null; } catch { return null; }
    }
    function save(s) { localStorage.setItem(KEY, JSON.stringify(s)); }
    function clear() { localStorage.removeItem(KEY); }

    function ensureBar() {
        if (document.getElementById('wmPlayer')) return document.getElementById('wmPlayer');
        const bar = document.createElement('div');
        bar.id = 'wmPlayer';
        bar.className = 'fixed-bottom bg-body-tertiary border-top p-2 d-none';
        bar.innerHTML = `
            <div class="container-fluid d-flex align-items-center gap-2 flex-wrap">
                <img id="wmCover" src="" alt="" class="wm-player-cover rounded" />
                <div class="flex-grow-1 wm-player-info">
                    <div id="wmTitle" class="small fw-semibold text-truncate"></div>
                    <audio id="wmAudio" controls class="w-100 wm-player-audio"></audio>
                </div>
                <button id="wmClose" type="button" class="btn btn-sm btn-outline-danger" aria-label="Đóng player">✕</button>
            </div>`;
        document.body.appendChild(bar);
        document.getElementById('wmClose').addEventListener('click', function () {
            const a = document.getElementById('wmAudio');
            a.pause(); a.removeAttribute('src'); a.load();
            bar.classList.add('d-none');
            clear();
        });
        return bar;
    }

    function play(src, title, cover, id) {
        const bar = ensureBar();
        const audio = document.getElementById('wmAudio');
        document.getElementById('wmCover').src = cover;
        document.getElementById('wmTitle').textContent = title;
        audio.src = src;
        audio.play().catch(() => {});
        bar.classList.remove('d-none');
        save({ src, title, cover, id, t: audio.currentTime });
        if (id) fetch('/BaiHat/Play', { method: 'POST', headers: { 'Content-Type': 'application/x-www-form-urlencoded' }, body: 'id=' + id }).catch(() => {});
        audio.addEventListener('timeupdate', function () {
            const s = load(); if (s) { s.t = audio.currentTime; save(s); }
        }, { passive: true });
    }

    window.wmPlay = play;

    document.addEventListener('DOMContentLoaded', function () {
        document.addEventListener('click', function (e) {
            const btn = e.target.closest('.wm-play, .wm-play-overlay, .wm-play-overlay-static');
            if (!btn) return;
            e.preventDefault();
            play(btn.dataset.src, btn.dataset.title, btn.dataset.cover, btn.dataset.id);
        });

        const s = load();
        if (s && s.src) {
            const bar = ensureBar();
            const audio = document.getElementById('wmAudio');
            document.getElementById('wmCover').src = s.cover;
            document.getElementById('wmTitle').textContent = s.title;
            audio.src = s.src;
            audio.currentTime = s.t || 0;
            bar.classList.remove('d-none');
            audio.addEventListener('timeupdate', function () {
                const cur = load(); if (cur) { cur.t = audio.currentTime; save(cur); }
            }, { passive: true });
        }
    });
})();