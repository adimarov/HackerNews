export default interface Story {
  id: number;
  by: string;
  descendants: number;
  kids: number[];
  score: number;
  time: string;
  title: string;
  type: string;
  url: string;
  text: string;
}
