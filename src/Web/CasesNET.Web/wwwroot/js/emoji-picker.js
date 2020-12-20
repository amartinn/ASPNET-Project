import { EmojiButton } from '/lib/emoji-button/dist/index.js';

const trigger = document.querySelector('.add-emoji');

const picker = new EmojiButton({
    style: 'twemoji'
});


trigger.addEventListener('click', () => {
    picker.togglePicker(trigger);
});
picker.on('emoji', selection => {
    addEmoji(selection.url);
});