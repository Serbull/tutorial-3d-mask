using UnityEngine;
using DG.Tweening;

public class ScannerDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _detectImage;
    [SerializeField] private GameObject _completeImage;

    public void StartDetect()
    {
        _detectImage.SetActive(true);
    }

    public void StopDetect()
    {
        _detectImage.SetActive(false);
    }

    public void Detecting()
    {
        _detectImage.transform.localRotation *= Quaternion.Euler(0, 0, -240 * Time.deltaTime);
    }

    public void CompleteDetect()
    {
        _detectImage.SetActive(false);
        _completeImage.SetActive(true);
        DOVirtual.DelayedCall(1f, () => _completeImage.SetActive(false));
    }
}