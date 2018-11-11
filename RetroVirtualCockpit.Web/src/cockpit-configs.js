import f16cp_st_am from './configs/f16cp_st_am'
import f19sf_st_am from './configs/f19sf_st_am'

export default function cockpit_configs() {
    return [
        f16cp_st_am(),
        f19sf_st_am()
    ]
}
