import { useEffect, useState } from "react";

const TranslateDropdown = () => {
  const [isLoaded, setIsLoaded] = useState(false);

  useEffect(() => {
    const addGoogleTranslateScript = () => {
      if (!document.querySelector("#google-translate-script")) {
        const script = document.createElement("script");
        script.id = "google-translate-script";
        script.src =
          "https://translate.google.com/translate_a/element.js?cb=googleTranslateElementInit";
        script.async = true;
        document.body.appendChild(script);
      }
    };

    window.googleTranslateElementInit = () => {
      new window.google.translate.TranslateElement(
        {
          pageLanguage: "en",
          includedLanguages: "ar,tr",
          layout: window.google.translate.TranslateElement.InlineLayout.SIMPLE,
        },
        "google_translate_element"
      );
      setIsLoaded(true);
    };

    addGoogleTranslateScript();
  }, []);

  return (
    <div className="flex flex-col items-center justify-center p-4">
      {!isLoaded && <p>Loading translation...</p>}
      <div id="google_translate_element" className="mt-4"></div>
    </div>
  );
};

export default TranslateDropdown;
