import { format, parseISO, subDays } from "date-fns";

const QUERY_DATE_FORMAT = "yyyy-MM-dd";
const DISPLAY_DATE_FORMAT = "dd/MM/yyyy";

export const formatDateForQuery = (date: Date) =>
  format(date, QUERY_DATE_FORMAT);

export const formatDateForDisplay = (date: Date) =>
  format(date, DISPLAY_DATE_FORMAT);

export const parseQueryDate = (value: string) => parseISO(value);

export const getDefaultDateRange = (today = new Date()) => ({
  FromDate: formatDateForQuery(subDays(today, 30)),
  ToDate: formatDateForQuery(today),
});