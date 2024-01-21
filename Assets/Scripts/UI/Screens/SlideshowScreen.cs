using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class SlideshowScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text _narratorText;
        [SerializeField] private float _textTypingSpeed = 0.05f;
        [SerializeField] private List<Image> _leftPhotos;
        [SerializeField] private List<Image> _rightPhotos;
        [SerializeField] private List<string> _narratorTexts;


        private int _currentIndex;
        private List<Vector3> _leftPhotosOriginalPositions = new List<Vector3>();
        private List<Vector3> _rightPhotosOriginalPositions = new List<Vector3>();
        private bool _isSlideshowFinished;

        public bool IsSlideshowFinished => _isSlideshowFinished;

        public void Start()
        {
            SaveOriginalPositions();
            StartCoroutine(StartSlideshow());
        }

        private void SaveOriginalPositions()
        {
            for (int i = 0; i < _leftPhotos.Count; i++)
            {
                _leftPhotosOriginalPositions.Add(_leftPhotos[i].gameObject.transform.localPosition);
            }

            for (int i = 0; i < _rightPhotos.Count; i++)
            {
                _rightPhotosOriginalPositions.Add(_rightPhotos[i].gameObject.transform.localPosition);
            }
        }

        public IEnumerator StartSlideshow()
        {
            yield return MovePhotosOntoCanvas();

            while (_currentIndex < _narratorTexts.Count)
            {
                yield return TypeNarratorText(_narratorTexts[_currentIndex]);

                yield return MovePhotosOutOfCanvas(_currentIndex);

                _currentIndex++;
            }

            _isSlideshowFinished = true;
            Debug.Log("Slideshow finished!");
        }

        private IEnumerator MovePhotosOntoCanvas()
        {
            for (int i = 0; i < _leftPhotos.Count; i++)
            {
                _leftPhotos[i].gameObject.transform.localPosition =
                    new Vector3(-3000f, _leftPhotosOriginalPositions[i].y, 0);
            }

            for (int i = 0; i < _rightPhotos.Count; i++)
            {
                _rightPhotos[i].gameObject.transform.localPosition =
                    new Vector3(3000f, _rightPhotosOriginalPositions[i].y, 0);
            }

            yield return new WaitForSeconds(1f);
        }

        private IEnumerator MovePhotosOutOfCanvas(int index)
        {
            Sequence mySequence = DOTween.Sequence();

            if (_leftPhotos.Count - 1 >= index)
            {
                mySequence.Append(_leftPhotos[index].gameObject.transform
                    .DOLocalMoveX(_leftPhotosOriginalPositions[index].x, 0.2f));
                mySequence.Play();
            }

            if (_rightPhotos.Count - 1 >= index)
            {
                mySequence.Append(_rightPhotos[index].gameObject.transform
                    .DOLocalMoveX(_rightPhotosOriginalPositions[index].x, 0.2f));
                mySequence.Play();
            }

            yield return new WaitForSeconds(1f);
        }

        private IEnumerator TypeNarratorText(string text)
        {
            _narratorText.text = "";

            foreach (char letter in text)
            {
                _narratorText.text += letter;
                yield return new WaitForSeconds(_textTypingSpeed);
            }

            yield return new WaitForSeconds(1f);
        }
    }
}