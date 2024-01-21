using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Storyline
{
    public class SlideshowController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _narratorText;
        [SerializeField] private float _textTypingSpeed = 0.05f;
        [SerializeField] private List<Image> _leftPhotos;
        [SerializeField] private List<Image> _rightPhotos;
        [SerializeField] private List<string> _narratorTexts;

        private int currentIndex;
        private List<Vector3> leftPhotosOriginalPositions = new List<Vector3>();
        private List<Vector3> rightPhotosOriginalPositions = new List<Vector3>();

        void Start()
        {
            // Сохраняем исходные позиции фотографий
            SaveOriginalPositions();

            StartCoroutine(StartSlideshow());
        }

        void SaveOriginalPositions()
        {
            for (int i = 0; i < _leftPhotos.Count; i++)
            {
                leftPhotosOriginalPositions.Add(_leftPhotos[i].gameObject.transform.localPosition);
            }

            for (int i = 0; i < _rightPhotos.Count; i++)
            {
                rightPhotosOriginalPositions.Add(_rightPhotos[i].gameObject.transform.localPosition);
            }
        }

        IEnumerator StartSlideshow()
        {
            yield return MovePhotosOntoCanvas();

            while (currentIndex < _narratorTexts.Count)
            {
                yield return TypeNarratorText(_narratorTexts[currentIndex]);

                yield return MovePhotosOutOfCanvas(currentIndex);

                currentIndex++;
            }

            Debug.Log("Slideshow finished!");
        }

        IEnumerator MovePhotosOntoCanvas()
        {
            for (int i = 0; i < _leftPhotos.Count; i++)
            {
                _leftPhotos[i].gameObject.transform.localPosition =
                    new Vector3(-3000f, leftPhotosOriginalPositions[i].y, 0);
            }

            for (int i = 0; i < _rightPhotos.Count; i++)
            {
                _rightPhotos[i].gameObject.transform.localPosition =
                    new Vector3(3000f, rightPhotosOriginalPositions[i].y, 0);
            }

            yield return new WaitForSeconds(1f);
        }

        IEnumerator MovePhotosOutOfCanvas(int index)
        {
            Sequence mySequence = DOTween.Sequence();

            if (_leftPhotos.Count - 1 >= index)
            {
                mySequence.Append(_leftPhotos[index].gameObject.transform
                    .DOLocalMoveX(leftPhotosOriginalPositions[index].x, 0.2f));
                mySequence.Play();
            }

            if (_rightPhotos.Count - 1 >= index)
            {
                mySequence.Append(_rightPhotos[index].gameObject.transform
                    .DOLocalMoveX(rightPhotosOriginalPositions[index].x, 0.2f));
                mySequence.Play();
            }

            yield return new WaitForSeconds(1f);
        }

        IEnumerator TypeNarratorText(string text)
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