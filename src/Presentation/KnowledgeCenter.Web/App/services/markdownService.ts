module App.Services {
    "use strict";

    var serviceId: string = "markdownService";

    declare var hljs: any;

    export interface IMarkdownService {
        createHtml(markdown: string): string;
    }

    export class MarkdownService implements IMarkdownService {
        constructor() {
            marked.setOptions({
                sanitize: false,
                breaks: true,
                highlight: function(code: string, language: string) {
                    if (language === "c#" || language === "csharp") {
                        language = "cs";
                    }
                    if (language === "Javascript") {
                        language = "js";
                    }
                    if (language && hljs.LANGUAGES[language]) {
                        if (language) {
                            return hljs.highlight(language, code).value;
                        } else {
                            return hljs.highlightAuto(code).value;
                        }
                    }
                }
            });
        }

        public createHtml(markdown: string): string {
            return marked(markdown);
        }

    }

    angular.module("knowledgeCenterApp")
        .factory(serviceId,() => {
        return new MarkdownService();
    });
} 