export const feedbackKeys = {
  feedbacks: {
    root: ["feedback"],
  },
  mutations: {
    create: () => [...feedbackKeys.feedbacks.root, "create"],
  },
};
