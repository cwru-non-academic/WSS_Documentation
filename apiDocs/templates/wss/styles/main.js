// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
//
// DocFX template overlay: render Mermaid fenced blocks (```mermaid) as diagrams.
// This repo intentionally loads Mermaid from a CDN for now.

(function () {
  'use strict';

  function loadScript(src) {
    return new Promise(function (resolve, reject) {
      var s = document.createElement('script');
      s.src = src;
      s.async = true;
      s.onload = resolve;
      s.onerror = reject;
      document.head.appendChild(s);
    });
  }

  function findMermaidBlocks() {
    return document.querySelectorAll('pre > code.lang-mermaid, pre > code.language-mermaid');
  }

  function convertMermaidBlocks(nodes) {
    for (var i = 0; i < nodes.length; i++) {
      var code = nodes[i];
      var pre = code.parentNode;
      if (!pre || !pre.parentNode) continue;
      var div = document.createElement('div');
      div.className = 'mermaid';
      div.textContent = code.textContent;
      pre.parentNode.replaceChild(div, pre);
    }
  }

  function ensureMermaidLoaded() {
    if (window.mermaid) return Promise.resolve();
    return loadScript('https://cdn.jsdelivr.net/npm/mermaid@10/dist/mermaid.min.js');
  }

  document.addEventListener('DOMContentLoaded', function () {
    var blocks = findMermaidBlocks();
    if (!blocks || blocks.length === 0) return;

    ensureMermaidLoaded()
      .then(function () {
        if (!window.mermaid) return;
        convertMermaidBlocks(blocks);
        window.mermaid.initialize({ startOnLoad: false });
        if (typeof window.mermaid.run === 'function') {
          window.mermaid.run({ querySelector: '.mermaid' });
        }
      })
      .catch(function () {
        // If Mermaid fails to load, keep blocks as plain code.
      });
  });
})();
