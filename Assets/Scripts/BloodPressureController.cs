using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class BloodPressureManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField systolicIF;
    public TMP_InputField diastolicIF;
    public TMP_InputField ageIF;
    public TMP_InputField genderIF;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI adviceTxt;

    [Header("Validation Error Texts (Under Textboxes)")]
    public TextMeshProUGUI systolicErrorTxt;
    public TextMeshProUGUI diastolicErrorTxt;
    public TextMeshProUGUI ageErrorTxt;
    public TextMeshProUGUI genderErrorTxt;

    [Header("Transitions")]
    public SceneTransition transitionScript;

    DatabaseReference dbReference;

    void Start()
    {
        Application.targetFrameRate = 60;
        dbReference = FirebaseDatabase.GetInstance("https://aorta-7390e-default-rtdb.europe-west1.firebasedatabase.app/").RootReference;

        ClearErrorMessages();
        LoadLastEntry();
    }
    private void ClearErrorMessages()
    {
        if (systolicErrorTxt != null) systolicErrorTxt.text = "";
        if (diastolicErrorTxt != null) diastolicErrorTxt.text = "";
        if (ageErrorTxt != null) ageErrorTxt.text = "";
        if (genderErrorTxt != null) genderErrorTxt.text = "";
    }

    public void OnBaselineClick()
    {
        bool isGreek = true;
        if (LocalizationManager.Instance != null)
        {
            isGreek = (LocalizationManager.Instance.currentLanguage == LocalizationManager.Language.Greek);
        }

        ClearErrorMessages();
        bool hasError = false;

        if (string.IsNullOrEmpty(systolicIF.text))
        {
            systolicErrorTxt.text = isGreek ? "Απαιτείται τιμή!" : "Required field!";
            hasError = true;
        }
        else if (!int.TryParse(systolicIF.text, out _))
        {
            systolicErrorTxt.text = isGreek ? "Μόνο αριθμοί!" : "Numbers only!";
            hasError = true;
        }

        if (string.IsNullOrEmpty(diastolicIF.text))
        {
            diastolicErrorTxt.text = isGreek ? "Απαιτείται τιμή!" : "Required field!";
            hasError = true;
        }
        else if (!int.TryParse(diastolicIF.text, out _))
        {
            diastolicErrorTxt.text = isGreek ? "Μόνο αριθμοί!" : "Numbers only!";
            hasError = true;
        }
        
        if (hasError) return;

        int sys = int.Parse(systolicIF.text);
        int dia = int.Parse(diastolicIF.text);

        string category = "";
        string advice = "";

        if (sys < 120 && dia < 80)
        {
            category = isGreek ? "ΦΥΣΙΟΛΟΓΙΚΗ" : "NORMAL";
            advice = isGreek ? "Τέλεια! Συνεχίστε τον υγιεινό τρόπο ζωής." : "Perfect! Keep up the healthy lifestyle.";
        }
        else if (sys >= 120 && sys <= 129 && dia < 80)
        {
            category = isGreek ? "ΑΥΞΗΜΕΝΗ" : "ELEVATED";
            advice = isGreek ? "Ελαφρώς υψηλή. Προσέξτε την πρόσληψη αλατιού." : "Slightly high. Watch your salt intake.";
        }
        else if ((sys >= 130 && sys <= 139) || (dia >= 80 && dia <= 89))
        {
            category = isGreek ? "ΥΠΕΡΤΑΣΗ ΣΤΑΔΙΟ 1" : "HYPERTENSION STAGE 1";
            advice = isGreek ? "Συμβουλευτείτε γιατρό για αλλαγές στον τρόπο ζωής." : "Consult a doctor for lifestyle changes.";
        }
        else if (sys >= 140 || dia >= 90)
        {
            category = isGreek ? "ΥΠΕΡΤΑΣΗ ΣΤΑΔΙΟ 2" : "HYPERTENSION STAGE 2";
            advice = isGreek ? "Οι τιμές είναι υψηλές. Παρακολουθήστε καθημερινά και ζητήστε συμβουλή." : "Values are high. Monitor daily and seek advice.";
        }
        else
        {
            category = isGreek ? "ΜΗ ΕΓΚΥΡΗ" : "INVALID";
            advice = isGreek ? "Ελέγξτε τις τιμές εισαγωγής." : "Check input values.";
        }

        if (sys > 180 || dia > 120)
        {
            category = isGreek ? "ΥΠΕΡΤΑΣΙΚΗ ΚΡΙΣΗ" : "HYPERTENSIVE CRISIS";
            advice = isGreek ? "ΕΠΕΙΓΟΝ: Επικοινωνήστε αμέσως με γιατρό." : "EMERGENCY: Contact a doctor immediately.";
        }

        DisplayResult(category, advice, sys, dia);
    }

    public void OnPersonalizedClick()
    {
        bool isGreek = true;
        if (LocalizationManager.Instance != null)
        {
            isGreek = (LocalizationManager.Instance.currentLanguage == LocalizationManager.Language.Greek);
        }

        ClearErrorMessages();
        bool hasError = false;

        if (string.IsNullOrEmpty(systolicIF.text))
        {
            systolicErrorTxt.text = isGreek ? "Απαιτείται τιμή!" : "Required field!";
            hasError = true;
        }
        else if (!int.TryParse(systolicIF.text, out _))
        {
            systolicErrorTxt.text = isGreek ? "Μόνο αριθμοί!" : "Numbers only!";
            hasError = true;
        }

        if (string.IsNullOrEmpty(diastolicIF.text))
        {
            diastolicErrorTxt.text = isGreek ? "Απαιτείται τιμή!" : "Required field!";
            hasError = true;
        }
        else if (!int.TryParse(diastolicIF.text, out _))
        {
            diastolicErrorTxt.text = isGreek ? "Μόνο αριθμοί!" : "Numbers only!";
            hasError = true;
        }

        if (string.IsNullOrEmpty(ageIF.text))
        {
            ageErrorTxt.text = isGreek ? "Εισάγετε ηλικία!" : "Enter age!";
            hasError = true;
        }
        else if (!int.TryParse(ageIF.text, out _))
        {
            ageErrorTxt.text = isGreek ? "Μόνο αριθμοί!" : "Numbers only!";
            hasError = true;
        }

        if (string.IsNullOrEmpty(genderIF.text))
        {
            genderErrorTxt.text = isGreek ? "Εισάγετε φύλο!" : "Enter gender!";
            hasError = true;
        }

        if (hasError) return;

        int sys = int.Parse(systolicIF.text);
        int dia = int.Parse(diastolicIF.text);
        int age = int.Parse(ageIF.text);
        string gender = genderIF.text.Trim().ToLower();

        string category = "";
        string advice = "";

        if (age >= 18 && age <= 39)
        {
            if (gender.Contains("f") || gender.Contains("γυν") || gender.Contains("w"))
            {
                if (sys <= 115 && dia <= 72)
                {
                    category = isGreek ? "ΙΔΑΝΙΚΗ (ΓΥΝΑΙΚΑ 18-39)" : "OPTIMAL (WOMAN 18-39)";
                    advice = isGreek ? "Εξαιρετική πίεση, πολύ κοντά στον ιδανικό μέσο όρο (110/68)." : "Excellent pressure, very close to the ideal average (110/68).";
                }
                else
                {
                    category = isGreek ? "ΑΥΞΗΜΕΝΗ (ΓΥΝΑΙΚΑ 18-39)" : "ELEVATED (WOMAN 18-39)";
                    advice = isGreek ? "Οι τιμές ξεπερνούν τον μέσο όρο της ηλικιακής σας ομάδας." : "Values exceed the average for your age group.";
                }
            }
            else
            {
                if (sys <= 122 && dia <= 75)
                {
                    category = isGreek ? "ΙΔΑΝΙΚΗ (ΑΝΔΡΑΣ 18-39)" : "OPTIMAL (MAN 18-39)";
                    advice = isGreek ? "Τυπική φυσιολογική πίεση για νέο άνδρα (Μέσος όρος: 119/70)." : "Typical normal pressure for a young man (Average: 119/70).";
                }
                else
                {
                    category = isGreek ? "ΑΥΞΗΜΕΝΗ (ΑΝΔΡΑΣ 18-39)" : "ELEVATED (MAN 18-39)";
                    advice = isGreek ? "Προειδοποίηση: Οι τιμές είναι υψηλότερες από τον μέσο όρο (119/70)." : "Warning: Values are higher than the average (119/70).";
                }
            }
        }
        else if (age >= 40 && age <= 59)
        {
            if (gender.Contains("f") || gender.Contains("γυν") || gender.Contains("w"))
            {
                if (sys <= 125 && dia <= 78)
                {
                    category = isGreek ? "ΦΥΣΙΟΛΟΓΙΚΗ (ΓΥΝΑΙΚΑ 40-59)" : "NORMAL (WOMAN 40-59)";
                    advice = isGreek ? "Η πίεσή σας είναι σταθερή και φυσιολογική (Μέσος όρος: 122/74)." : "Your pressure is stable and normal (Average: 122/74).";
                }
                else
                {
                    category = isGreek ? "ΑΥΞΗΜΕΝΗ (ΓΥΝΑΙΚΑ 40-59)" : "ELEVATED (WOMAN 40-59)";
                    advice = isGreek ? "Ελαφρά αύξηση πάνω από τον αναμενόμενο μέσο όρο (122/74)." : "Slight increase above the expected average (122/74).";
                }
            }
            else
            {
                if (sys <= 128 && dia <= 80)
                {
                    category = isGreek ? "ΦΥΣΙΟΛΟΓΙΚΗ (ΑΝΔΡΑΣ 40-59)" : "NORMAL (MAN 40-59)";
                    advice = isGreek ? "Καλή πίεση, εντός των ορίων της ηλικίας σας (Μέσος όρος: 124/77)." : "Good pressure, within the limits of your age (Average: 124/77).";
                }
                else
                {
                    category = isGreek ? "ΑΥΞΗΜΕΝΗ (ΑΝΔΡΑΣ 40-59)" : "ELEVATED (MAN 40-59)";
                    advice = isGreek ? "Τιμές υψηλότερες από τον μέσο όρο της ηλικίας σας (124/77)." : "Values higher than your age average (124/77).";
                }
            }
        }
        else if (age >= 60)
        {
            if (gender.Contains("f") || gender.Contains("γυν") || gender.Contains("w"))
            {
                if (sys <= 139 && dia <= 80)
                {
                    category = isGreek ? "ΦΥΣΙΟΛΟΓΙΚΗ (ΓΥΝΑΙΚΑ 60+)" : "NORMAL (WOMAN 60+)";
                    advice = isGreek ? "Οι τιμές είναι εξαιρετικές και κοντά στον μέσο όρο (139/68)." : "Values are excellent and close to the average (139/68).";
                }
                else
                {
                    category = isGreek ? "ΑΥΞΗΜΕΝΗ (ΓΥΝΑΙΚΑ 60+)" : "ELEVATED (WOMAN 60+)";
                    advice = isGreek ? "Υπέρβαση του μέσου όρου ηλικίας. Συμβουλευτείτε γιατρό." : "Above the age average. Please consult your doctor.";
                }
            }
            else
            {
                if (sys <= 133 && dia <= 80)
                {
                    category = isGreek ? "ΦΥΣΙΟΛΟΓΙΚΗ (ΑΝΔΡΑΣ 60+)" : "NORMAL (MAN 60+)";
                    advice = isGreek ? "Οι τιμές είναι εντός του μέσου όρου για την ηλικία σας (133/69)." : "Values are within the average for your age (133/69).";
                }
                else
                {
                    category = isGreek ? "ΑΥΞΗΜΕΝΗ (ΑΝΔΡΑΣ 60+)" : "ELEVATED (MAN 60+)";
                    advice = isGreek ? "Υπάρχει απόκλιση από τον μέσο όρο ηλικίας (133/69)." : "There is a deviation from the age average (133/69).";
                }
            }
        }
        else
        {
            category = isGreek ? "ΠΑΙΔΙΑΤΡΙΚΗ ΟΜΑΔΑ" : "PEDIATRIC GROUP";
            advice = isGreek ? "Παρακαλώ συμβουλευτείτε παιδίατρο για τα φυσιολογικά όρια." : "Please consult a pediatrician for normal pediatric limits.";
        }
        DisplayResult(category, advice, sys, dia);
    }

    void DisplayResult(string cat, string adv, int s, int d)
    {
        resultText.text = cat;
        adviceTxt.text = adv;

        if (transitionScript != null) transitionScript.ShowResults();

        SaveData(s, d, cat);
    }

    void SaveData(int sys, int dia, string cat)
    {
        PlayerPrefs.SetInt("LastSys", sys);
        PlayerPrefs.SetInt("LastDia", dia);
        PlayerPrefs.SetString("LastCat", cat);
        PlayerPrefs.Save();

        int savedAge = 0;
        if (!string.IsNullOrEmpty(ageIF.text))
        {
            int.TryParse(ageIF.text, out savedAge);
        }

        string savedGender = string.IsNullOrEmpty(genderIF.text) ? "N/A" : genderIF.text;

        UserData data = new UserData(sys, dia, savedAge, savedGender, cat);
        string json = JsonUtility.ToJson(data);
        dbReference.Child("Entries").Push().SetRawJsonValueAsync(json);
    }
    void LoadLastEntry()
    {
        if (PlayerPrefs.HasKey("LastSys"))
        {
            systolicIF.text = PlayerPrefs.GetInt("LastSys").ToString();
            diastolicIF.text = PlayerPrefs.GetInt("LastDia").ToString();
            resultText.text = PlayerPrefs.GetString("LastCat");
        }
    }
}