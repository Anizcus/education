import { GetterTree, ActionTree, Module, MutationTree } from "vuex";
import { Language, LanguageModel } from "@/assets/i18n/language";

const namespaced = true;
const defaultLanguage = "Lithuanian";
const dateOptions = {
  weekday: "long",
  year: "numeric",
  month: "long",
  day: "numeric",
  hour: "2-digit",
  minute: "2-digit",
  hour12: false
};

interface LanguageStore {
  language: LanguageModel;
  available: string[];
  key: string;
}

const mutations: MutationTree<LanguageStore> = {
  setLanguage(state, language: LanguageModel) {
    state.language = language;
  },
  setKey(state, key: string) {
    state.key = key;
  }
};

const getters: GetterTree<LanguageStore, {}> = {
  getTranslations(state) {
    return state.language;
  },
  getKey(state) {
    return state.key;
  }
};

const actions: ActionTree<LanguageStore, {}> = {
  setTranslations(context, key: string) {
    const translations = Object.entries(Language).find(item => {
      return item[0] == key;
    });

    const language = translations ? translations[1] : Language.Lithuanian;

    const languageKey = translations ? translations[0] : defaultLanguage;

    context.commit("setLanguage", language);
    context.commit("setKey", languageKey);
  },
  getLocalTime(context, date: string) {
    const locale = context.state.key == "Lithuanian" ? "lt-LT" : "en-EN";

    return new Date(date).toLocaleDateString(locale, dateOptions);
  }
};

const state: LanguageStore = {
  language: Language.Lithuanian,
  key: defaultLanguage,
  available: Object.keys(Language)
};

const languageStore: Module<LanguageStore, {}> = {
  state,
  getters,
  actions,
  mutations,
  namespaced
};

export default languageStore;
