using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    public enum Language { Greek, English }
    [Header("Current Language State")]
    public Language currentLanguage = Language.Greek;

    [Header("UI Text Components (TextMeshPro)")]
    public TMP_Text welcomeText;
    public TMP_Text editionText;
    public TMP_Text titleText;
    public TMP_Text underTitleMainText;
    public TMP_Text buttonMainText;
    public TMP_Text systolicLabel;
    public TMP_Text diastolicLabel;
    public TMP_Text ageLabel;
    public TMP_Text genderLabel;
    public TMP_Text baselineBtnText;
    public TMP_Text personalizedBtnText;
    public TMP_Text saveBtnText;
    public TMP_Text Top2NDtext;
    public TMP_Text under2NDtext;
    public TMP_Text resultText;
    public Image langImage;
    public Image langImage2;
    public Sprite GreeceImage;
    public Sprite USAImage;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        int savedLanguage = PlayerPrefs.GetInt("AppLanguage", 0);
        currentLanguage = (Language)savedLanguage;

        UpdateUI_Language();
    }

    public void ToggleLanguage()
    {
        if (currentLanguage == Language.Greek)
            currentLanguage = Language.English;
        else
            currentLanguage = Language.Greek;

        PlayerPrefs.SetInt("AppLanguage", (int)currentLanguage);
        PlayerPrefs.Save();

        UpdateUI_Language();
    }

    public void UpdateUI_Language()
    {
        if (currentLanguage == Language.Greek)
        {
            resultText.text = "Αποτέλεσμα";
            welcomeText.text = "ΚΑΛΩΣΟΡΙΣΜΑ";
            editionText.text = "ΕΚΔΟΣΗ 2026";
            titleText.text = "Aorta™\nΕφαρμογή";
            underTitleMainText.text = "Προηγμένη ανάλυση για μια πιο υγιή\r\nκαρδιά. Ξεκινήστε το ταξίδι σας προς\r\nτην ακριβή βιομετρική παρακολούθηση σήμερα.";
            buttonMainText.text = "Ξεκινήστε";
            Top2NDtext.text = "Η καρδιά σου, αποκωδικοποιημένη.";
            under2NDtext.text = "Εισαγάγετε τα ζωτικά σας στοιχεία για να ξεκινήσετε.";
            systolicLabel.text = "Εισάγετε τη συστολική πίεση...";
            diastolicLabel.text = "Εισαγωγή Διαστολικής Πίεσης...";
            ageLabel.text = "Εισαγάγετε την ηλικία...";
            genderLabel.text = "Εισαγάγετε το φύλο...";
            baselineBtnText.text = "Βασικός Έλεγχος";
            personalizedBtnText.text = "Εξατομικευμένος";
            langImage.sprite = USAImage;
            langImage2.sprite = USAImage;
        }
        else
        {
            resultText.text = "Your Result";
            welcomeText.text = "WELCOME";
            editionText.text = "2026 EDITION";
            underTitleMainText.text = "Advanced analytics for a healthier\r\nheart. Start your journey towards\r\nprecise biometric tracking today.";
            buttonMainText.text = "Get Started";
            titleText.text = "Aorta™\nApplication";
            Top2NDtext.text = "Your heart, decoded.";
            under2NDtext.text = "Enter your vitals to begin.";
            systolicLabel.text = "Enter Systolic Pressure...";
            diastolicLabel.text = "Enter Diastolic Pressure...";
            ageLabel.text = "Enter Age...";
            genderLabel.text = "Enter Gender...";
            baselineBtnText.text = "Baseline Assessment";
            personalizedBtnText.text = "Personalized Analysis";
            langImage.sprite = GreeceImage;
            langImage2.sprite = GreeceImage;
        }
    }
}