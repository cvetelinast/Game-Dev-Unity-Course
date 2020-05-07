using UnityEngine;

public class ThreeDigitsController : MonoBehaviour {

    [SerializeField] private GameObject firstDigit;
    [SerializeField] private GameObject secondDigit;
    [SerializeField] private GameObject thirdDigit;

    public void changeValue(int newValue) {
        int third = newValue % 10;
        newValue /= 10;
        int second = newValue % 10;
        newValue /= 10;
        int first = newValue;

        if (first == 0) {
            firstDigit.SetActive(false);
        } else {
            firstDigit.SetActive(true);
        }

        if (first == 0 && second == 0) {
            secondDigit.SetActive(false);
        } else {
            secondDigit.SetActive(true);
        }

        firstDigit.GetComponent<Animator>().SetInteger("Value", first);
        secondDigit.GetComponent<Animator>().SetInteger("Value", second);
        thirdDigit.GetComponent<Animator>().SetInteger("Value", third);
    }
}
