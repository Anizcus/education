const dateOptions = {
   weekday: "long",
   year: "numeric",
   month: "long",
   day: "numeric",
   hour: "2-digit",
   minute: "2-digit",
   hour12: false
};

const getLocalTime = (language: string, date: string) => {
   const locale = language == "Lithuanian" ? "lt-LT" : "en-EN";

   return new Date(date).toLocaleDateString(locale, dateOptions);
}

export { getLocalTime, dateOptions }