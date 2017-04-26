using System.Collections;

public class DungeonNameGenerator {
    /*
    FIXME_VAR_TYPE name_set = { };
    FIXME_VAR_TYPE chain_cache = { };
    
    void generate_name(type) {
        FIXME_VAR_TYPE chain;

        if(chain = markov_chain(type)) {
            return markov_name(chain);
        }

        return '';
    }

    void name_list(type, n_of) {
        FIXME_VAR_TYPE list = [];

        for(FIXME_VAR_TYPE i = 0; i < n_of; i++) {
            list.push(generate_name(type));
        }

        return list;
    }
    
    void markov_chain(type) {
        FIXME_VAR_TYPE chain;

        if(chain = chain_cache[type]) {
            return chain;
        }
        else {
            FIXME_VAR_TYPE list;

            if(list = name_set[type]) {
                FIXME_VAR_TYPE chain;

                if(chain = construct_chain(list)) {
                    chain_cache[type] = chain;
                    return chain;
                }
            }
        }

        return false;
    }

    void construct_chain(list) {
        FIXME_VAR_TYPE chain = { };
        
        for(FIXME_VAR_TYPE i = 0; i < list.length; i++) {
            FIXME_VAR_TYPE names = list[i].split(/\s +/);
            chain = incr_chain(chain, 'parts', names.length);
            
            for(FIXME_VAR_TYPE j = 0; j < names.length; j++) {
                FIXME_VAR_TYPE name = names[j];
                chain = incr_chain(chain, 'name_len', name.length);

                FIXME_VAR_TYPE c = name.substr(0, 1);
                chain = incr_chain(chain, 'initial', c);

                FIXME_VAR_TYPE string= name.substr(1);
                FIXME_VAR_TYPE last_c = c;

                while(string.length > 0) {
                    FIXME_VAR_TYPE c = string.substr(0, 1);
                    chain = incr_chain(chain, last_c, c);

                    string = string.substr(1);
                    last_c = c;
                }
            }
        }

        return scale_chain(chain);
    }

    void incr_chain(chain, key, token) {
        if(chain[key]) {
            if(chain[key][token]) {
                chain[key][token]++;
            }
            else {
                chain[key][token] = 1;
            }
        }
        else {
            chain[key] = { };
            chain[key][token] = 1;
        }

        return chain;
    }

    void scale_chain(chain) {
        FIXME_VAR_TYPE table_len = { };
        
        foreach(FIXME_VAR_TYPE key in chain) {
            table_len[key] = 0;
            
            for(FIXME_VAR_TYPE token in chain[key]) {
                FIXME_VAR_TYPE count = chain[key][token];
                FIXME_VAR_TYPE weighted = Math.floor(Math.pow(count, 1.3f));
                chain[key][token] = weighted;
                table_len[key] += weighted;
            }
        }

        chain['table_len'] = table_len;

        return chain;
    }

    void markov_name(chain) {
        FIXME_VAR_TYPE parts = select_link(chain, 'parts');
        FIXME_VAR_TYPE names = [];
        
        for(FIXME_VAR_TYPE i = 0; i < parts; i++) {
            FIXME_VAR_TYPE name_len = select_link(chain, 'name_len');
            FIXME_VAR_TYPE c = select_link(chain, 'initial');
            FIXME_VAR_TYPE name = c;
            FIXME_VAR_TYPE last_c = c;

            while(name.length < name_len) {
                c = select_link(chain, last_c);
                name += c;
                last_c = c;
            }

            names.push(name);
        }

        return names.join(' ');
    }

    void select_link(chain, key) {
        FIXME_VAR_TYPE len = chain['table_len'][key];
        FIXME_VAR_TYPE idx = Math.floor(Math.random() * len);
        
        for(FIXME_VAR_TYPE token in chain[key]) {
            t += chain[key][token];

            if(idx < t) {
                return token;
            }
        }

        return '-';
    }
    */
}