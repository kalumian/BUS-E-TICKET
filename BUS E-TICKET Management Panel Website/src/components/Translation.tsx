import { useEffect } from 'react';

declare global {
  interface Window {
    googleTranslateElementInit: () => void;
  }

  interface Window {
    google: {
      translate: {
        TranslateElement: {
          new (options: any, container: string): void;
          InlineLayout: {
            SIMPLE: any;
          };
        };
      };
    };
  }

  interface Google { // This can be removed if not used elsewhere
    translate: {
      TranslateElement: new (options: any, container: string) => void & {
        InlineLayout: {
          SIMPLE: any;
        };
      };
    };
  }

}

const GoogleTranslate = () => {
  useEffect(() => {
    // تعريف الدالة في النافذة العالمية
    window.googleTranslateElementInit = () => {
      new window.google.translate.TranslateElement(
        { pageLanguage: 'en', includedLanguages: 'tr', layout: window.google.translate.TranslateElement.InlineLayout.SIMPLE },
        'google_translate_element'
      );
    };

    // إضافة سكريبت Google Translate
    const addScript = document.createElement('script');
    addScript.src = "//translate.google.com/translate_a/element.js?cb=googleTranslateElementInit";
    addScript.async = true;
    document.body.appendChild(addScript);

    return () => {
      // تنظيف عند إلغاء تحميل المكون (اختياري)
      document.body.removeChild(addScript);
    };
  }, []);

  return <div id="google_translate_element"></div>;
};

export default GoogleTranslate;
